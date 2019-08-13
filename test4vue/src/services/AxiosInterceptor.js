import axios from "axios";
import qs from "qs";
import api from "./ApiClient"
import Auth from './AuthService'

export function refreshTokenInterceptor(domain, getRefreshToken, handleResponse) {
    api.interceptors.response.use(
        function (response) {
            return response;
        },
        function (error) {
            const originalRequest = error.config;
            if (
                typeof error.response !== "undefined" &&
                (error.response.status === 403 || error.response.status === 401)
            ) {
                if (!originalRequest._retry) {
                    originalRequest._retry = true;
                    var bodyParam =
                        "grant_type=refresh_token&client_id=client&client_secret=secret&refresh_token=" +
                        getRefreshToken();
                    return axios
                        .post(`${domain}connect/token`, bodyParam, {
                            headers: {
                                "Content-Type": "application/x-www-form-urlencoded"
                            }
                        })
                        .then(response => {
                            handleResponse(response.data);
                            let token = response.data.access_token;
                            if (token && token.length > 0) {
                                originalRequest.headers['Authorization'] = "Bearer " + token;
                            }
                            else {
                                originalRequest.headers['Authorization'] = null;
                            }

                            return api(originalRequest);
                        })
                        .catch(e => {
                            //TODO: catch error
                            Auth.logout();
                            window.location = '/'
                        });
                } else {
                    //TODO: catch error
                    Auth.logout();
                    window.location = '/'
                }
            }
            return Promise.reject(error);
        }
    );
}

export function addStoreIdsInterceptor(getStoreIds) {
    api.interceptors.request.use(config => {
        config.params = { sids: getStoreIds() };
        config.paramsSerializer = params => {
            return qs.stringify(params);
        };
        return config;
    });
}
