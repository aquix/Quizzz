export default class AppCtrl {
    /** @ngInject */
    constructor($mdSidenav, userName, route) {
        this._$mdSidenav = $mdSidenav;
        this.userName = userName;
        this.route = route;
    }

    toggleRight() {
        this._$mdSidenav('left').toggle();
    };
}