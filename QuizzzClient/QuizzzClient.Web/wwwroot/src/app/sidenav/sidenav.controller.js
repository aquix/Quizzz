export default class SidenavCtrl {
    constructor($mdSidenav, route, $http) {
        this._$mdSidenav = $mdSidenav;
        this._route = route;
        this._$http = $http;

        this.links = [
            {
                name: 'Play quizzes',
                routeName: 'play.popular'
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
        this._route.go(link.routeName, link.name);
    }

    extLink(link) {
        this._route.goExternal(link);
    }

    logout() {
        this._$http.post('/account/logoff').then(() => {
            this._route.goExternal('/');
        }).catch(res => {
            this._route.error(res.data);
        });
    }

    close() {
        this._$mdSidenav('left').close();
    };
}