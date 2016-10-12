export default class AppCtrl {
    constructor($mdSidenav) {
        this._$mdSidenav = $mdSidenav;

        this.menuItems = ['Hello', 'World']
    }

    toggleRight() {
        this._$mdSidenav('left').toggle();
    };
}