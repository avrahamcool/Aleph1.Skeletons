/** number of MS in 1 second */
export const second = 1000;

/** number of seconds in 1 minute */
export const secondsInMinute = 60;

/** number of MS in 1 minute */
export const minute: number = second * secondsInMinute;

/** number of minutes in 1 hour */
export const minutesInHour = 60;

/** number of MS in 1 hour */
export const hour: number = minute * minutesInHour;

/** number of hours in 1 day */
export const hoursInDay = 24;

/** number of MS in 1 day */
export const day: number = hour * hoursInDay;


/** number of MS in each time scale */
export const timeByScale = {
	second,
	minute,
	hour,
	day
};
