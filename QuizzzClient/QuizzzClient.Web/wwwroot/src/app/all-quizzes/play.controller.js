export default class PlayQuizzesCtrl {
    constructor($state) {
        this._$state = $state;

        this.currentRoute = 'popular';
        this.go('popular');
    }

    go(routeName) {
        this.currentRoute = routeName;
        this._$state.go(`play.${routeName}`);
    }
}