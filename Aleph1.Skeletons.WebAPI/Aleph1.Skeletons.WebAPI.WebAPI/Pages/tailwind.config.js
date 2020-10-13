module.exports = {
	future: {
		removeDeprecatedGapUtilities: true,
		purgeLayersByDefault: true,
	},
	purge: {
		enabled: true,	//true in prod, false otherwise (changed inside webpack.config)
		content: [
			"./index.ejs",
			"./src/**/*.html",
			"./src/**/*.ts"
		]
	},
	theme: {
		extend: {},
	},
	variants: {},
	plugins: [],
}
