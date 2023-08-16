namespace LogGate
{
    using System;

    public class Subscription
    {
        public Subscription() { }
        public Subscription(int id, int customerId, int monthlyPriceInCents)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.MonthlyPriceInCents = monthlyPriceInCents;
        }

        public int Id;
        public int CustomerId;
        public int MonthlyPriceInCents;
    }

    public class User
    {
        public User() { }
        public User(int id, string name, DateTime activatedOn, DateTime deactivatedOn, int customerId)
        {
            this.Id = id;
            this.Name = name;
            this.ActivatedOn = activatedOn;
            this.DeactivatedOn = deactivatedOn;
            this.CustomerId = customerId;
        }

        public int Id;
        public string Name;
        public DateTime ActivatedOn;
        public DateTime DeactivatedOn;
        public int CustomerId;
    }

    public class Challenge
    {
        /// Computes the monthly charge for a given subscription.
        ///
        /// @returns The total monthly bill for the customer in cents, rounded
        /// to the nearest cent. For example, a bill of $20.00 should return 2000.
        /// If there are no active users or the subscription is null, returns 0.
        ///
        /// @param month - Always present
        ///   Has the following structure:
        ///   "2022-04"  // April 2022 in YYYY-MM format
        ///
        /// @param subscription - May be null
        ///   If present, has the following structure (see Subscription class):
        ///   {
        ///     Id: 763,
        ///     CustomerId: 328,
        ///     MonthlyPriceInCents: 359  // price per active user per month
        ///   }
        ///
        /// @param users - May be empty, but not null
        ///   Has the following structure (see User class):
        ///   [
        ///     {
        ///       Id: 1,
        ///       Name: "Employee #1",
        ///       CustomerId: 1,
        ///
        ///       // when this user started
        ///       ActivatedOn: new Date("2021-11-04"),
        ///
        ///       // last day to bill for user
        ///       // should bill up to and including this date
        ///       // since user had some access on this date
        ///       DeactivatedOn: new Date("2022-04-10")
        ///     },
        ///     {
        ///       Id: 2,
        ///       Name: "Employee #2",
        ///       CustomerId: 1,
        ///
        ///       // when this user started
        ///       ActivatedOn: new Date("2021-12-04"),
        ///
        ///       // hasn't been deactivated yet
        ///       DeactivatedOn: DateTime.MaxValue
        ///     },
        ///   ]
        public static int MonthlyCharge(string month, Subscription subscription, User[] users)
        {
            foreach (var user in users)
            {
                DateTime monthDtg = DateTime.ParseExact(month, "yyyy-MM", null);

                if (subscription is null)
                    continue;

                decimal dailyCharge = Math.Round(subscription.MonthlyPriceInCents / 100.0m, 2);
                dailyCharge = Math.Round(dailyCharge / DateTime.DaysInMonth(monthDtg.Year, monthDtg.Month), 2);



                int daysActive = NumberofDaysActive(monthDtg, user.ActivatedOn);

                Console.WriteLine($"Plan: {subscription.MonthlyPriceInCents}");
            }
            return -1;

            int NumberofDaysActive(DateTime dtg, DateTime activatedOn)
            {
                if (dtg.Month < activatedOn.Month && dtg.Year < activatedOn.Year)
                    return 0;
                int daysInMonth = DateTime.DaysInMonth(dtg.Year, dtg.Month);
                if (dtg.Month == activatedOn.Month)
                {
                    TimeSpan timeSpan = LastDayOfMonth(dtg) - activatedOn;
                    return timeSpan.Days;
                }
                return DateTime.DaysInMonth(dtg.Year, dtg.Month);
            }
        }

        /*******************
        * Helper functions *
        *******************/

        /**
        Takes a DateTime object and returns a DateTime which is the first day
        of that month. For example:

        FirstDayOfMonth(new DateTime(2022, 3, 17)) // => new DateTime(2022, 3, 1)
        **/
        private static DateTime FirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /**
        Takes a DateTime object and returns a DateTime which is the last day
        of that month. For example:

        LastDayOfMonth(new DateTime(2022, 3, 17)) // => new DateTime(2022, 3, 31)
        **/
        private static DateTime LastDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /**
        Takes a DateTime object and returns a DateTime which is the next day.
        For example:

        NextDay(new DateTime(2022, 3, 17)) // => new DateTime(2022, 3, 18)
        NextDay(new DateTime(2022, 3, 31)) // => new DateTime(2022, 4, 1)
        **/
        private static DateTime NextDay(DateTime date)
        {
            return date.AddDays(1);
        }
    }
}
