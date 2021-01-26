import IMask from "imask/esm/imask";

export class Admin
{
	public value = "123";
	public options: IMask.AnyMaskedOptions = {
		mask: /^ז?\d{0,9}$/
	};
}
