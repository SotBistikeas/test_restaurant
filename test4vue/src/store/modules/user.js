import apiClient from "@/services/ApiClient";
export default {

  namespaced: true,
  state: {
    user: {
      userName: 'User Name',
      name: 'Name',
      surname: 'Surname',
      emailAddress: 'Email'
    }
  },
  mutations: {
    setUserData(state, payload) {
      state.user = payload;
    }
  },
  actions: {
    updateCurrentUser({ commit, state }) {

      apiClient.get("/Session/GetCurrentLoginInformations")
        .then((resp) => {
          commit('setUserData', resp.data.result.user)
        })

    }
  }
};
