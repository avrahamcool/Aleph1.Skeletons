export function outerHeight(element: HTMLElement): number
{
	const height = element.offsetHeight;
	const style = window.getComputedStyle(element);

	const marginTop = parseInt(style.marginTop, 10);
	const marginBottom = parseInt(style.marginBottom, 10);
	return height + marginTop + marginBottom;
}
