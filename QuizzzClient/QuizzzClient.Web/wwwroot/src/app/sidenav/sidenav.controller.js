export default class SidenavCtrl {
    constructor($mdSidenav, $state) {
        this._$mdSidenav = $mdSidenav;
        this._$state = $state;

        this.links = [
            {
                name: 'Play quizzes',
                routeName: 'play'
            },
            {
                name: 'My stats',
                routeName: 'stats'
            },
            {
                name: 'Add quiz',
                routeName: 'addQuiz'
            }
        ]
    }

    click(link) {
        this._$state.go(link.routeName);
    }

    close() {
        this._$mdSidenav('left').close();
    };
}