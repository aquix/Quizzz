export default class ApiService {
    /** @ngInject */
    constructor (apiPath, Upload, $http) {
        this.apiPath = apiPath;
        this.Upload = Upload;
        this.$http = $http;
    }

    addQuizz(file) {
        return this.Upload.upload({
            url: `${this.apiPath}`,
            data: {
                file: file
            }
        })
    }

    getPopular(count) {
        return this.$http.get(`${this.apiPath}/previews/${count}`);
    }
}