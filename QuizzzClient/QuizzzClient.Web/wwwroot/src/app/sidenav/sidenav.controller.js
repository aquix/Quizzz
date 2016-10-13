export default class SidenavCtrl {
    constructor($mdSidenav, $state, $window, $http) {
        this._$mdSidenav = $mdSidenav;
        this._$state = $state;
        this._$window = $window;
        this._$http = $http;

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

    extLink(link) {
        this._$window.location.href = link;
    }

    logout() {
        this._$http.post('/account/logoff')
            .then(() => {
                this._$window.location.href = '/';
            })
    }

    close() {
        this._$mdSidenav('left').close();
    };
}