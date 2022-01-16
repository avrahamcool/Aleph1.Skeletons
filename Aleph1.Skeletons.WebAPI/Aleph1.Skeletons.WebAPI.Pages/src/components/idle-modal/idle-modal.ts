import { autoinject } from "aurelia-framework";
import { DialogController } from "aurelia-dialog";
import { second } from "resources/helpers";
import { default as environment } from "../../../config/environment.json";

@autoinject()
export class IdleModal
{
	constructor(private dialogController: DialogController)
	{
		this.intervalHandler = window.setInterval(() =>
		{
			if (--this.secondsTillSignOut === 0)
			{
				this.closeHandler(false);
			}
		}, second);
	}

	public secondsTillSignOut = environment.idleWarningDurationInSec;
	private intervalHandler: number;

	public closeHandler(ok: boolean): void
	{
		window.clearInterval(this.intervalHandler);

		if (ok)
		{
			this.dialogController.ok();
		}
		else
		{
			this.dialogController.cancel();
		}
	}
}
