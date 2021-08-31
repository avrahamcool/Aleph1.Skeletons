const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const DuplicatePackageCheckerPlugin = require("duplicate-package-checker-webpack-plugin");
const { AureliaPlugin, ModuleDependenciesPlugin } = require("aurelia-webpack-plugin");
const { BundleAnalyzerPlugin } = require("webpack-bundle-analyzer");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const Tailwind = require("tailwindcss");
const TailwindConfig = require("./tailwind.config");
const Autoprefixer = require("autoprefixer");
const CssNano = require("cssnano");
const project = require("./aurelia_project/aurelia.json");

// Helpers:
const ensureArray = config => (config && (Array.isArray(config) ? config : [config])) || [];
const when = (condition, config, negativeConfig) => (condition ? ensureArray(config) : ensureArray(negativeConfig));

// Paths and addresses:
const outDir = path.resolve(__dirname, project.platform.output);
const srcDir = path.resolve(__dirname, "src");
const baseUrl = "";

module.exports = function (env, { analyze, tests, hot, port, host, https } = {})
{
	const production = !env.WEBPACK_SERVE;

	TailwindConfig.purge.enabled = production;

	const postCssPlugins = [
		Tailwind({ config: TailwindConfig }),
		...(production ? [Autoprefixer, CssNano] : [])
	];

	const cssRules = [
		{ loader: "css-loader", options: { esModule: false } },
		{ loader: "postcss-loader", options: { postcssOptions: { plugins: postCssPlugins } } }
	];

	const cssLoader = [
		production ? { loader: MiniCssExtractPlugin.loader } : "style-loader"
	];

	const sassRules = [
		{ loader: "sass-loader", options: { sassOptions: { includePaths: ["node_modules"] } } }
	];

	return {
		stats: "minimal",
		target: "web",
		entry: { app: ["aurelia-bootstrapper"] },
		mode: production ? "production" : "development",
		cache: { type: "memory" },
		output: {
			path: outDir,
			pathinfo: !production,
			publicPath: baseUrl,
			filename: production ? "[name].[chunkhash].bundle.js" : "[name].[contenthash].bundle.js",
			chunkFilename: production ? "[name].[chunkhash].chunk.js" : "[name].[contenthash].chunk.js",
			sourceMapFilename: production ? "[name].[chunkhash].bundle.map" : "[name].[contenthash].bundle.map"
		},
		resolve: {
			extensions: [".ts", ".js"],
			modules: [srcDir, "node_modules"],
			alias: {
				"aurelia-binding": path.resolve(__dirname, "node_modules/aurelia-binding")
			}
		},
		devtool: production ? false : "eval-cheap-module-source-map",
		devServer: {
			client: {
				overlay: true,
				progress: true
			},
			compress: true,
			historyApiFallback: true,
			host,
			hot: hot || project.platform.hot,
			https,
			open: project.platform.open,
			port: port || project.platform.port,
			setupExitSignals: true,
			static: {
				directory: outDir,
				publicPath: baseUrl
			},
			webSocketServer: "ws"
		},
		plugins: [
			...when(!tests, new DuplicatePackageCheckerPlugin()),
			new AureliaPlugin(),
			new ModuleDependenciesPlugin({
				"aurelia-testing": ["./compile-spy", "./view-spy"]
			}),
			new HtmlWebpackPlugin({
				template: "index.ejs", metadata: { baseUrl }
			}),
			new MiniCssExtractPlugin({
				filename: production ? "[name].[contenthash].bundle.css" : "[name].bundle.css",
				chunkFilename: production ? "[name].[contenthash].chunk.css" : "[name].chunk.css"
			}),
			...when(!tests, new CopyWebpackPlugin({
				patterns: [{ from: "static", to: outDir, globOptions: { ignore: [".*"] } }]
			})),
			...when(analyze, new BundleAnalyzerPlugin()),
			...when(production, new CleanWebpackPlugin())
		],
		module: {
			rules: [
				{
					enforce: "pre",
					test: /\.html$/i,
					use: [{ loader: "aurelia-template-check-loader", options: { emitErrors: true, typeChecking: true } }]
				},
				{ test: /\.css$/i, issuer: { not: [ /\.html$/i ] }, use: [...cssLoader, ...cssRules] },
				{ test: /\.css$/i, issuer: /\.html$/i, use: cssRules },
				{ test: /\.scss$/, issuer: /\.[tj]s$/i, use: [...cssLoader, ...cssRules, ...sassRules] },
				{ test: /\.scss$/, issuer: /\.html?$/i, use: [...cssRules, ...sassRules] },
				{ test: /\.html$/i, loader: "html-loader" },
				{ test: /\.ts$/, loader: "ts-loader" },
				{ test: /\.(png|gif|jpg|cur)$/i, type: "asset" },
				{ test: /\.(woff2?|ttf|eot|svg|otf)(\?v=[0-9]\.[0-9]\.[0-9])?$/i, type: "asset/resource" },
				{ test: /environment\.json$/i, use: [{ loader: "app-settings-loader", options: { env: production ? "production" : "development" } }] },
				...when(tests, {
					test: /\.[jt]s$/i,
					loader: "istanbul-instrumenter-loader",
					include: srcDir,
					exclude: [/\.(spec|test)\.[jt]s$/i],
					enforce: "post",
					options: { esModules: true }
				})
			]
		},
		optimization: {
			runtimeChunk: "single",
			moduleIds: production ? "deterministic" : "named",
			splitChunks: {
				chunks: "initial",
				hidePathInfo: true,
				maxInitialRequests: Infinity,
				maxAsyncRequests: Infinity,
				minSize: 10000,
				maxSize: 50000,
				usedExports: true,
				cacheGroups: {
					default: false,
					vendor: {
						name: "vendor", priority: 21, test: /[\\/]node_modules[\\/]/
					},
					vendors: {
						name: "vendors", priority: 20, test: /[\\/]node_modules[\\/]/, enforce: true
					},
					asyncVendor: {
						name: "vendor.async", priority: 11, test: /[\\/]node_modules[\\/]/, chunks: "async"
					},
					asyncVendors: {
						name: "vendors.async", priority: 10, test: /[\\/]node_modules[\\/]/, chunks: "async", enforce: true
					},
					common: {
						name: "common", priority: 1, minChunks: 2, chunks: "initial"
					},
					commons: {
						name: "commons", priority: 0, minChunks: 2, chunks: "initial", enforce: true
					},
					styles: {
						name: "styles", priority: 0, type: "css/mini-extract", chunks: "all"
					}
				}
			}
		}
	};
};