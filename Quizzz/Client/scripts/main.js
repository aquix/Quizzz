function MainViewModel() {
    var self = this;

    self.author = ko.observable('');
    self.question = ko.observable('');
    self.answers = ko.observableArray([]);
    self.category = ko.observable('');

    self.generateQuizz = function () {
        var data = ko.toJSON(self);
        return;
    }

    self.addAnswer = function () {
        self.answers.push(new AnswerViewModel());
    }

    self.removeAnswer = function (answer) {
        self.answers.remove(answer);
    }
}

function AnswerViewModel() {
    var self = this;

    self.isCorrect = ko.observable(false);
    self.answerString = ko.observable('');
}

function init() {
    var mainViewModel = new MainViewModel();
    mainViewModel.answers.push(new AnswerViewModel());
    ko.applyBindings(mainViewModel);
}

init();