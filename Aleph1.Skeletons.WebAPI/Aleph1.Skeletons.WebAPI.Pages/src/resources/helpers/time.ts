// Number of milliseconds in 1 second
export const second = 1000;

// Number of seconds in 1 minute
export const secondsInMinute = 60;

// Number of milliseconds in 1 minute
export const minute: number = second * secondsInMinute;

// Number of minutes in 1 hour
export const minutesInHour = 60;

// Number of milliseconds in 1 hour
export const hour: number = minute * minutesInHour;

// Number of hours in 1 day
export const hoursInDay = 24;

// Number of milliseconds in 1 day
export const day: number = hour * hoursInDay;

// Number of milliseconds in each time scale
export const timeByScale = {
	second,
	minute,
	hour,
	day
};
