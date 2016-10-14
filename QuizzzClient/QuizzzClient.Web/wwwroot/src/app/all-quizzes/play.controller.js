export default class PlayQuizzesCtrl {
    /** @ngInject */
    constructor(route) {
        this.route = route;
        route.currentPageTitle = 'Play quizzes';

        this.currentRoute = 'popular';
        this.go('popular');
    }

    go(routeName) {
        this.currentRoute = routeName;
        this.route.go(`play.${routeName}`, 'Play quizzes');
    }
}