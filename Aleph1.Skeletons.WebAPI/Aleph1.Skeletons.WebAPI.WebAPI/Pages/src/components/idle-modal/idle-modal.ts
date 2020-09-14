import { second } from "resources/services";
import { autoinject } from "aurelia-framework";
import { DialogController } from "aurelia-dialog";
import * as environment from "../../../config/environment.json";

@autoinject
export class IdleModal
{
	public secondsTillLogout = environment.idleWarningDurationSec;
	public intervalHandler: number;

	constructor(private dialogController: DialogController)
	{
		this.intervalHandler = window.setInterval(() =>
		{
			if (--this.secondsTillLogout === 0)
			{
				this.close(false);
			}
		}, second);
	}

	close(ok: boolean): void
	{
		clearInterval(this.intervalHandler);
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
