// eslint-disable-next-line import/no-extraneous-dependencies
const plugin = require("tailwindcss/plugin");

module.exports = {
	mode: "jit",
	future: {
		removeDeprecatedGapUtilities: true,
		purgeLayersByDefault: true
	},
	purge: {
		enabled: true,
		content: [
			"./index.ejs",
			"./src/**/*.{ts,html}"
		]
	},
	theme: {
		fontFamily: {
			inherit: "inherit",
			sans: [
				"assistant",
				"arial",
				"sans-serif"
			],
			mono: [
				"cousine",
				"'courier new'",
				"monospace"
			]
		},
		extend: {
			borderRadius: {
				1: "0.25rem",
				2: "0.5rem",
				3: "0.75rem",
				4: "1rem",
				5: "1.25rem",
				6: "1.5rem"
			},
			cursor: {
				"zoom-in": "zoom-in",
				"zoom-out": "zoom-out"
			},
			height: {
				fit: "fit-content"
			},
			maxHeight: {
				screen: "100vh",
				fit: "fit-content",
				xs: "20rem",
				sm: "24rem",
				md: "28rem",
				lg: "32rem",
				xl: "36rem"
			},
			minHeight: {
				screen: "100vh",
				fit: "fit-content",
				xs: "20rem",
				sm: "24rem",
				md: "28rem",
				lg: "32rem",
				xl: "36rem"
			},
			width: {
				fit: "fit-content"
			},
			maxWidth: {
				screen: "100vw",
				fit: "fit-content",
				xs: "20rem",
				sm: "24rem",
				md: "28rem",
				lg: "32rem",
				xl: "36rem"
			},
			minWidth: {
				screen: "100vw",
				fit: "fit-content",
				xs: "20rem",
				sm: "24rem",
				md: "28rem",
				lg: "32rem",
				xl: "36rem"
			}
		}
	},
	variants: {
		extend: {}
	},
	plugins: [
		plugin(({ addComponents, addUtilities }) =>
		{
			addUtilities({
				".adjust-padding": {
					paddingBottom: ".125rem"
				},
				".content-null": {
					content: "''"
				},
				".dir-ltr": {
					direction: "ltr"
				},
				".dir-rtl": {
					direction: "rtl"
				},
				".fa-spin-faster": {
					animation: "fa-spin 1s infinite linear"
				},
				".flex-center": {
					display: "flex",
					alignItems: "center",
					justifyContent: "center"
				},
				".flex-start": {
					display: "flex",
					alignItems: "flex-start",
					justifyContent: "flex-start"
				},
				".flex-baseline-start": {
					display: "flex",
					alignItems: "baseline",
					justifyContent: "flex-start"
				},
				".flex-center-start": {
					display: "flex",
					alignItems: "center",
					justifyContent: "flex-start"
				},
				".flex-center-end": {
					display: "flex",
					alignItems: "center",
					justifyContent: "flex-end"
				},
				".flex-start-center": {
					display: "flex",
					alignItems: "flex-start",
					justifyContent: "center"
				},
				".flex-start-end": {
					display: "flex",
					alignItems: "flex-start",
					justifyContent: "flex-end"
				},
				".flex-end-center": {
					display: "flex",
					alignItems: "flex-end",
					justifyContent: "center"
				},
				".flex-end-start": {
					display: "flex",
					alignItems: "flex-end",
					justifyContent: "flex-start"
				},
				".flex-center-between": {
					display: "flex",
					alignItems: "center",
					justifyContent: "space-between"
				},
				".flex-start-between": {
					display: "flex",
					alignItems: "flex-start",
					justifyContent: "space-between"
				},
				".flip-horizontally": {
					transform: "scale(-1, 1)"
				},
				".flip-vertically": {
					transform: "scale(1, -1)"
				},
				".focusable": {
					transition: "box-shadow 150ms cubic-bezier(0.4, 0, 0.2, 1)",
					"&:focus-visible": {
						boxShadow: "0 0 0 2px currentColor"
					}
				},
				".line-clamp-2": {
					display: "-webkit-box",
					"-webkit-line-clamp": "2",
					"-webkit-box-orient": "vertical"
				},
				".line-clamp-3": {
					display: "-webkit-box",
					"-webkit-line-clamp": "3",
					"-webkit-box-orient": "vertical"
				},
				".line-clamp-4": {
					display: "-webkit-box",
					"-webkit-line-clamp": "4",
					"-webkit-box-orient": "vertical"
				},
				".preserve-3d": {
					transformStyle: "preserve-3d"
				},
				".reset-root": {
					"-webkit-margin-before": "0",
					"-webkit-margin-end": "0",
					"-webkit-margin-after": "0",
					"-webkit-margin-start": "0",
					"-webkit-padding-before": "0",
					"-webkit-padding-end": "0",
					"-webkit-padding-after": "0",
					"-webkit-padding-start": "0",
					"-ms-overflow-style": "none",
					"-webkit-text-size-adjust": "100%",
					textSizeAdjust: "100%",
					"-webkit-font-smoothing": "antialiased",
					fontSmooth: "always"
				},
				".scroll-behavior-smooth": {
					scrollBehavior: "smooth"
				},
				".scrollbar-w-none": {
					scrollbarWidth: "none"
				},
				".tap-transparent": {
					"-webkit-tap-highlight-color": "transparent"
				},
				".text-transform-none": {
					textTransform: "none"
				},
				".webkit-appearance-none": {
					"-webkit-appearance": "none"
				}
			});
			addUtilities({
				".dialog-full": {
					width: "100vw",
					height: "100vh"
				},
				".dialog-full-2": {
					width: "calc(100vw - 4rem)",
					height: "calc(100vh - 4rem)"
				},
				".dialog-full-4": {
					width: "calc(100vw - 8rem)",
					height: "calc(100vh - 8rem)"
				},
				".dialog-full-6": {
					width: "calc(100vw - 12rem)",
					height: "calc(100vh - 12rem)"
				},
				".dialog-full-8": {
					width: "calc(100vw - 16rem)",
					height: "calc(100vh - 16rem)"
				},
				".dialog-full-10": {
					width: "calc(100vw - 20rem)",
					height: "calc(100vh - 20rem)"
				},
				".dialog-full-12": {
					width: "calc(100vw - 24rem)",
					height: "calc(100vh - 24rem)"
				},
				".sub-dialog-full-2": {
					width: "calc(100vw - 8rem)",
					height: "calc(100vh - 4rem)"
				},
				".sub-dialog-full-4": {
					width: "calc(100vw - 16rem)",
					height: "calc(100vh - 8rem)"
				},
				".sub-dialog-full-6": {
					width: "calc(100vw - 24rem)",
					height: "calc(100vh - 12rem)"
				},
				".sub-dialog-full-8": {
					width: "calc(100vw - 32rem)",
					height: "calc(100vh - 16rem)"
				},
				".sub-dialog-full-10": {
					width: "calc(100vw - 40rem)",
					height: "calc(100vh - 20rem)"
				},
				".sub-dialog-full-12": {
					width: "calc(100vw - 48rem)",
					height: "calc(100vh - 24rem)"
				}
			}, [
				"responsive"
			]);
			addComponents({
				".tw-shadow": {
					boxShadow: "0 0 #000000, 0 1px 3px 0 rgba(0, 0, 0, .3), 0 1px 2px 0 rgba(0, 0, 0, .1)"
				},
				".tw-shadow-strong": {
					boxShadow: "0 0 #000000, 0 1px 3px 0 rgba(0, 0, 0, .5), 0 1px 2px 0 rgba(0, 0, 0, .25)"
				},
				".tw-shadow-dialog": {
					boxShadow: "0 4px 12px 0 rgba(0, 0, 0, .3), 0 1px 4px 0 rgba(0, 0, 0, .2)"
				},
				".tw-button": {
					position: "relative",
					display: "flex",
					alignItems: "center",
					justifyContent: "center",
					padding: "0",
					height: "2.5rem",
					gap: ".5rem",
					userSelect: "none",
					outline: "0 solid transparent",
					appearance: "none",
					opacity: "1",
					whiteSpace: "nowrap",
					cursor: "pointer",
					pointerEvents: "auto",
					fontFamily: "inherit",
					fontWeight: "500",
					fontSize: "1rem",
					lineHeight: "1.5rem",
					borderWidth: "1px",
					borderStyle: "solid",
					borderColor: "rgb(31, 41, 55)",
					borderRadius: ".5rem",
					backgroundColor: "rgb(31, 41, 55)",
					color: "white",
					"&:disabled, &[data-pending='true']": {
						cursor: "default",
						pointerEvents: "none"
					},
					"&:disabled:not([data-pending='true'])": {
						borderColor: "rgb(156, 163, 175)",
						backgroundColor: "rgb(156, 163, 175)",
						color: "white"
					},
					"&::after": {
						content: "''",
						position: "absolute",
						top: "-3px",
						left: "-3px",
						bottom: "-3px",
						right: "-3px",
						borderRadius: "inherit",
						boxShadow: "none",
						transition: "box-shadow 150ms cubic-bezier(0.4, 0, 0.2, 1)"
					},
					"&:focus-visible::after": {
						boxShadow: "0 0 0 2px rgb(31, 41, 55)"
					},
					"&.button-outline": {
						borderColor: "rgb(31, 41, 55)",
						backgroundColor: "white",
						color: "rgb(31, 41, 55)",
						"&:disabled:not([data-pending='true'])": {
							borderColor: "rgb(156, 163, 175)",
							backgroundColor: "white",
							color: "rgb(156, 163, 175)"
						}
					}
				},
				".tw-input": {
					position: "relative",
					display: "block",
					height: "2.5rem",
					width: "100%",
					paddingLeft: "1rem",
					paddingRight: "1rem",
					fontFamily: "inherit",
					fontWeight: "500",
					fontSize: "1rem",
					textAlign: "start",
					lineHeight: "1.5rem",
					color: "rgb(31, 41, 55)",
					backgroundColor: "transparent",
					borderWidth: "1px",
					borderStyle: "solid",
					borderColor: "rgb(156, 163, 175)",
					borderRadius: ".5rem",
					outline: "0 solid transparent",
					appearance: "none",
					opacity: "1",
					resize: "none",
					pointerEvents: "auto",
					transition: "border-color 150ms cubic-bezier(0.4, 0, 0.2, 1)",
					"&:dir(rtl)": {
						textAlign: "right"
					},
					"&:dir(ltr)": {
						textAlign: "left"
					},
					"&::placeholder": {
						color: "rgb(156, 163, 175)"
					},
					"&:focus": {
						borderColor: "rgb(31, 41, 55)"
					}
				}
			});
		})
	]
};