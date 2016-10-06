let popularQuizzesTemplateUrl = require('ngtemplate!html!./popular-quizzes/popular-quizzes.html');
let allQuizzesTemplateUrl = require('ngtemplate!html!./all-quizzes/all-quizzes.html');
let quizzTemplateUrl = require('ngtemplate!html!./quizz/quizz.html');
let addQuizzTemplateUrl = require('ngtemplate!html!./add-quizz/add-quizz.html');
let statsTemplateUrl = require('ngtemplate!html!./stats/stats.html');

/* @ngInject */
export default function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/');

    $stateProvider
        .state('index', {
            url: '/',
            templateUrl: popularQuizzesTemplateUrl,
            controller: 'PopularQuizzesCtrl as ctrl'
        })
        .state('allQuizzes', {
            url: '/allQuizzes',
            templateUrl: allQuizzesTemplateUrl,
            controller: 'AllQuizzesController as ctrl'
        })
        .state('quizz', {
            url: '/quizz/:id',
            templateUrl: quizzTemplateUrl,
            controller: 'QuizzViewCtrl as ctrl'
        })
        .state('addQuizz', {
            url: '/addQuizz',
            templateUrl: addQuizzTemplateUrl,
            controller: 'AddQuizzCtrl as ctrl'
        })
        .state('stats', {
            url: '/stats',
            templateUrl: statsTemplateUrl,
            controller: 'StatsCtrl as ctrl'
        });

}