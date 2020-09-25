module.exports = {
	future: {
		removeDeprecatedGapUtilities: true,
		purgeLayersByDefault: true,
	},
	purge: {
		enabled: true,
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
