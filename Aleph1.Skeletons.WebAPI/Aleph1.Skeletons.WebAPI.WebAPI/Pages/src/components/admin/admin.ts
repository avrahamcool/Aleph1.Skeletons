export class Admin
{
	test = "DEFSULT";
	activate(params)
	{
		this.test = JSON.stringify(params);
	}
}
