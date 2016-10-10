export default class ApiService {
    /** @ngInject */
    constructor (apiPath, Upload, $http) {
        this.apiPath = apiPath;
        this.Upload = Upload;
        this.$http = $http;
    }

    addQuiz(file) {
        return this.Upload.upload({
            url: `${this.apiPath}`,
            data: {
                file: file
            }
        })
    }

    getPreviews(count, category="") {
        return this.$http.get(`${this.apiPath}/previews/${count}`, {
            params: {
                category: category
            }
        });
    }

    getQuiz(id) {
        return this.$http.get(`${this.apiPath}/${id}`);
    }

    acceptQuiz(data) {
        return this.$http.post(`${this.apiPath}/accept`, data);
    }
}