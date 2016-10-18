export default class StatsCtrl {
    /** @ngInject */
    constructor(api) {
        this.results = [];

        api.getStats().then(res => {
            this.results = res.data
        }).catch(res => {
            this._route.error(res.data);
        });
    }
}