import VatService from '@/services/VatService.js';

export const state = {
  vat: []
};
export const actions = {
  fetchVat({ commit, dispatch, state },) {
    return VatService.getVat(state)
      .then(response => {
        commit('SET_VAT', response.data);
      })
      .catch(error => {
        const notification = {
          type: 'error',
          message: 'there was a problem fetching vat: ' + error.message
        };
        dispatch('notification/add', notification, { root: true });
      });
  },
}