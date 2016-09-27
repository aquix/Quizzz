function MainViewModel() {
    var self = this;

    self.quizz = ko.observable({});
    self.outputType = ko.observable('json');

    self.generateQuizz = function () {
        var jsonData = ko.toJSON(self);
        location = '/generate?jsonData=' + jsonData;
    }

    self.addAnswer = function () {
        self.quizz().answers.push(new AnswerViewModel());
    }

    self.removeAnswer = function (answer) {
        self.quizz().answers.remove(answer);
    }
}

function NewQuizzViewModel() {
    var self = this;

    self.author = ko.observable('');
    self.question = ko.observable('');
    self.answers = ko.observableArray([]);
    self.category = ko.observable('');
}

function AnswerViewModel() {
    var self = this;

    self.isCorrect = ko.observable(false);
    self.answerString = ko.observable('');
}

function init() {
    var mainViewModel = new MainViewModel();
    mainViewModel.quizz(new NewQuizzViewModel());
    mainViewModel.quizz().answers.push(new AnswerViewModel());
    ko.applyBindings(mainViewModel);
}

init();