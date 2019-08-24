import decode from "jwt-decode";
import api from "./ApiClient"
import * as AxiosInterceptor from "./AxiosInterceptor";
//import "whatwg-fetch";
import axios from "axios";
import qs from "qs";

class AuthService {
    // Initializing important variables
    constructor(domain) {
        this.domain = domain || 'http://localhost:21021'; // API server domain
        this.fetch = this.fetch.bind(this); // React binding stuff
        this.login = this.login.bind(this);
        this.getProfile = this.getProfile.bind(this);
        this.getStoreIdsString = this.getStoreIdsString.bind(this);
        this.getAuthorizationHeader = this.getAuthorizationHeader.bind(this);
        this.getToken = this.getToken.bind(this);
        this.setTokens = this.setTokens.bind(this);
        AxiosInterceptor.refreshTokenInterceptor(this.domain, this.getRefreshToken, this.setTokens);
        AxiosInterceptor.addStoreIdsInterceptor(this.getStoreIdsString);
        //api.defaults.headers.common["Authorization"] = this.getAuthorizationHeader();
    }

    login(username, password) {
        const axiosConfig = {
            baseURL: 'http://localhost:21021',
            timeout: 30000,
            headers: {
                'Abp.TenantId': null,
                'Content-Type': 'application/json'
            }
        };

        const requestData = {

            usernameOrEmailAddress: username,
            password: password
        };


        return axios.post('/api/TokenAuth/Authenticate', requestData, axiosConfig).then(res => {
            if (res.data.success) {
                return Promise.resolve(res.data.result);
                return this.setTokens(res.data.result);
            } else {
                return Promise.reject(res);
            }
        })


    }

    register(data) {
        const axiosConfig = {
            baseURL: 'http://localhost:21021',
            headers: {
                'Abp.TenantId': null,
                'Content-Type': 'application/json'
            }
        };




        return axios.post('/api/services/app/Account/Register', data, axiosConfig).then(res => {
            if (res.data.success) {
                return Promise.resolve(res.data.result);
                return this.setTokens(res.data.result);
            } else {
                return Promise.reject(res);
            }
        })
    }



    getUserData() {
        return localStorage.getItem("userData") !== null && JSON.parse(localStorage.getItem("userData"));
    }
    getUserId() {
        return localStorage.getItem("userId");
    }
    getApplicationId() {
        return localStorage.getItem("applicantId");
    }
    getRoles() {
        return localStorage.getItem("roles") !== null ? JSON.parse(localStorage.getItem("roles")) : [];
    }
    getPermissions() {
        return localStorage.getItem("permissions") !== null ? JSON.parse(localStorage.getItem("permissions")) : [];
    }
    getPermissionsByStoreId(storeId) {
        if (localStorage.getItem("permissionsPerStore") === null) {
            return [];
        }
        let permissionsPerStore = JSON.parse(localStorage.getItem("permissionsPerStore"));
        return permissionsPerStore[storeId];
    }
    isInRole(role) {
        var r = this.getRoles();
        return Array.isArray(r) && r.filter(c => c.toLowerCase() == role.toLowerCase()).length > 0;
    }
    hasPermission(perm) {
        var r = this.getPermissions();
        return Array.isArray(r) && r.filter(c => c.toLowerCase() == perm.toLowerCase()).length > 0;
    }
    hasPermissionForAllStoreIds(perm, storeids) {
        return Array.isArray(storeids) && !storeids.some(storeid => !this.hasPermissionForStoreId(perm, storeid));
    }
    hasPermissionForStoreId(perm, storeid) {
        var r = this.getPermissionsByStoreId(storeid);
        return Array.isArray(r) && r.filter(c => c.toLowerCase() == perm.toLowerCase()).length > 0;
    }
    getFullName() {
        return this.getUserData().fullName;
    }
    getFirstName() {
        return this.getUserData().firstName;
    }
    getLastName() {
        return this.getUserData().lastName;
    }
    getDisplayRoles() {
        return this.getRoles().join(",");
    }
    getDisplayPermissions() {
        return this.getPermissions().join(",");
    }
    getStoreIdsString() {
        let ud = this.getUserData();
        if (!ud) return "";
        if (!ud.storeIds) return "";
        return ud.storeIds;
        return `[${ud.storeIds.join(",")}]`;
    }
    getStores() {
        let ud = this.getUserData();
        if (!ud) return [];
        return ud.stores;
    }
    getStoreOwnerId() {
        if (this.getStores().length == 0) return null;
        return this.getStores()[0].storeOwnerId;
    }
    getStoreOwnerIdFromStorage() {
        var userData = localStorage.getItem("userData");
        var parsedUserData = JSON.parse(userData);
        return parsedUserData.storeOwnerId;
    }
    getFirstStore() {
        if (this.getStores().length == 0) return null;
        return this.getStores()[0].id;
    }
    getStoreById(storeId) {
        return this.getStores().find(s => s.id == storeId);
    }

    loggedIn() {
        // Checks if there is a saved token and it's still valid
        const token = this.getToken(); // GEtting token from localstorage
        return !!token; // && !this.isTokenExpired(token); // handwaiving here
    }

    isTokenExpired(token) {
        try {
            const decoded = decode(token);
            if (decoded.exp < Date.now() / 1000) {
                // Checking if token is expired. N
                return true;
            } else return false;
        } catch (err) {
            return false;
        }
    }

    updateTokens() {
        var bodyParam = "grant_type=refresh_token&client_id=client&client_secret=secret&refresh_token=" + this.getRefreshToken();
        return axios
            .post(`${this.domain}connect/token`, bodyParam, {
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            })
            .then(response => {
                return this.setTokens(response.data);
            });
    }

    setTokens(responseToken) {
        localStorage.setItem("applicantAccessToken", responseToken.accessToken);
        localStorage.setItem("encryptedAccessToken", responseToken.encryptedAccessToken);
        localStorage.setItem("expireInSeconds", responseToken.expireInSeconds);
        localStorage.setItem("userId", responseToken.userId);

        //api.defaults.headers.common["Authorization"] = this.getAuthorizationHeader();

        // //call ajax to fetch applicant prefs
        // return api.get("services/app/Account/GetUserProperties").then(prefs => {
        //     var userData = prefs.data.result;
        //     localStorage.setItem("userData", JSON.stringify(userData));

        //     localStorage.setItem("userId", userData.userId);
        //     localStorage.setItem("applicantId", userData.applicantId);
        //     localStorage.setItem("roles", JSON.stringify(userData.roles));
        //     localStorage.setItem("permissions", JSON.stringify(userData.permissions));
        //     localStorage.setItem("permissionsPerStore", JSON.stringify(userData.permissionsPerStore));
        // });
    }

    getAuthorizationHeader() {
        let token = this.getToken();

        return token && token.length > 0 ? "Bearer " + token : null;
    }

    getToken() {
        // Retrieves the user token from localStorage
        return localStorage.getItem("applicantAccessToken");
    }

    getRefreshToken() {
        // Retrieves the user token from localStorage
        return localStorage.getItem("applicantRefreshToken");
    }

    logout() {
        // Clear user token and profile data from localStorage
        // localStorage.removeItem("applicantAccessToken");
        // localStorage.removeItem("applicantRefreshToken");
        // localStorage.removeItem("userData");
        // localStorage.removeItem("userId");
        // localStorage.removeItem("applicantId");
        // localStorage.removeItem("roles");
        // localStorage.removeItem("permissions");
        // localStorage.removeItem("permissionsPerStore");
        localStorage.clear();
    }

    getProfile() {
        // Using jwt-decode npm package to decode the token
        return decode(this.getToken());
    }

    fetch(url, options) {


        // performs api calls sending the required authentication headers
        const headers = {
            Accept: "application/json",
            "Content-Type": "application/json",

        };

        // Setting Authorization header
        // Authorization: Bearer xxxxxxx.xxxxxxxx.xxxxxx
        if (this.loggedIn()) {
            let token = this.getAuthorizationHeader();
            if (token && token.length > 0) {
                //headers["Authorization"] = "Bearer " + token;
            }
        }

        return fetch(url, {
            headers,
            ...options
        })
            .then(this._checkStatus)
            .then(response => response.json());
    }

    _checkStatus(response) {
        // raises an error in case response status is not a success
        if (response.status >= 200 && response.status < 300) {
            // Success status lies between 200 to 300
            return response;
        } else {
            var error = new Error(response.statusText);
            error.response = response;
            throw error;
        }
    }
}

export default new AuthService();
