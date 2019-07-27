import UnitOfMeasureService from '@/services/UnitOfMeasureService.js';

export const actions = {
  fetchUnits({ commit, dispatch, state }) {
    return UnitOfMeasureService.getUnit(state)
      .then(response => {
        commit('SET_UNITS', response.data.result.items);
      })
      .catch(error => {
        const notification = {
          type: 'error',
          message: 'there was a problem fetching units: ' + error.message
        };
        dispatch('notification/add', notification, { root: true });
        console.log('There was an error from unit.js');
      });
  }
};
