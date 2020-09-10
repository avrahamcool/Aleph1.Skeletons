import { autoinject } from "aurelia-framework";
import { DialogController } from "aurelia-dialog";
import * as environment from "../../../../config/environment.json";

const SECOND = 1000;

@autoinject
export class Idle
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
		}, SECOND);
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
