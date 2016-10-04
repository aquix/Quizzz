"use strict";

module.exports = {
    entry: {
        site: "./wwwroot/src/site.js",
        index: "./wwwroot/src/index.js"
    },
    output: {
        filename: "./wwwroot/dist/[name].js"
    },
    module: {
        loaders: [
            {
                test: /\.js$/,
                loader: "babel-loader"
            },
            {
                test: /\.scss$/,
                loader: "style!css!sass"
            }
        ]
    },
    devtool: 'source-map'
};