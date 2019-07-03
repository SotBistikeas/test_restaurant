import UnitService from '@/services/UnitOfMeasureService.js';

export const state = {
  unit: []
};
export const actions = {
  fetchUnit({ commit, dispatch, state },) {
    return UnitService.getUnit(state)
      .then(response => {
        commit('SET_UNIT', response.data);
      })
      .catch(error => {
        const notification = {
          type: 'error',
          message: 'there was a problem fetching units: ' + error.message
        };
        dispatch('notification/add', notification, { root: true });
      });
  },
}