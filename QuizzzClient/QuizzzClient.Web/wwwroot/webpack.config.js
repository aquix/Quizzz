"use strict";
let webpack = require('webpack');

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
                exclude: /(node_modules|lib)/,
                loader: "babel-loader"
            },
            {
                test: /\.scss$/,
                loader: "style!css!sass"
            }
        ]
    },
    plugins: [
        new webpack.ContextReplacementPlugin(/moment[\/\\]locale$/, /en$/)
    ],
    devtool: 'source-map'
};