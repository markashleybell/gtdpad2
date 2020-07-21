const _ = require('webpack');
const path = require('path');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

const config = {
    entry: './src/index.ts',
    output: {
        path: path.resolve(__dirname, 'wwwroot/js'),
        filename: '[name].js'
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader'
            },
            {
                test: /\.js$/,
                use: 'babel-loader',
                exclude: /node_modules/
            },
            {
                test: /\.ts(x)?$/,
                loader: 'ts-loader',
                exclude: /node_modules/,
                options: {
                    appendTsSuffixTo: [
                        /\.vue$/
                    ]
                }
            }
        ]
    },
    resolve: {
        extensions: [
            '.js',
            '.vue',
            '.tsx',
            '.ts'
        ]
    },
    plugins: [
        new CopyWebpackPlugin({
            patterns: [
                { from: './node_modules/jquery/dist/jquery.slim.min.js', to: '../js/jquery.slim.min.js' },
                { from: './node_modules/jquery/dist/jquery.slim.min.map', to: '../js/jquery.slim.min.map' },
                { from: './node_modules/bootstrap/dist/js/bootstrap.bundle.min.js', to: '../js/bootstrap.bundle.min.js' },
                { from: './node_modules/bootstrap/dist/js/bootstrap.bundle.min.js.map', to: '../js/bootstrap.bundle.min.js.map' },
                { from: './node_modules/bootstrap/dist/css/bootstrap.min.css', to: '../css/bootstrap.min.css' },
                { from: './node_modules/bootstrap/dist/css/bootstrap.min.css.map', to: '../css/bootstrap.min.css.map' }
            ]
        }),
        new VueLoaderPlugin(),
        new CleanWebpackPlugin()
    ],
    performance: {
        assetFilter: function (assetFilename) {
            return !assetFilename.endsWith('.map');
        }
    },
    optimization: {
        runtimeChunk: 'single',
        splitChunks: {
            cacheGroups: {
                vendor: {
                    test: /[\\/]node_modules[\\/]/,
                    name: 'vendors',
                    chunks: 'all'
                }
            }
        }
    }
};

module.exports = config;
