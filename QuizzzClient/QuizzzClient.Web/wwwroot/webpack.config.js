"use strict";

module.exports = {
    entry: {
        site: "./src/site.js",
        index: "./src/index.js"
    },
    output: {
        filename: "./dist/[name].js"
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