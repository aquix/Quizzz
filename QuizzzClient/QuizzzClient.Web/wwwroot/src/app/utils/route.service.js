export default class RouteService {
    /** @ngInject */
    constructor($state, $window) {
        this._$state = $state;
        this._$window = $window;
        this.currentPageTitle = "";
    }

    go(routeName, title, params) {
        this._$state.go(routeName, params);
        this.currentPageTitle = title;
    }

    goExternal(link) {
        this._$window.location.href = link;
    }
}