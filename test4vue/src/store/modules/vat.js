import VatService from '@/services/VatService.js';

export const actions = {
  fetchUnits({ commit, dispatch, state }) {
    return VatService.getVat(state)
      .then(response => {
        commit('SET_VATS', response.data);
      })
      .catch(error => {
        const notification = {
          type: 'error',
          message: 'there was a problem fetching Vat: ' + error.message
        };
        dispatch('notification/add', notification, { root: true });
        console.log('There was an error from Vat.js');
      });
  },
}