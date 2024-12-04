using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagementSystem.Application.Constants
{
    public static class Messages
    {
        public static string VehicleUsageHoursErrorMessage = "Active Hours + Maintenance Hours cannot be above weekly total hours(7x24)";
        public static string VehicleUsageSuccessfullyEnteredMessage = "Vehicle Usage Successfully Entered";
        public static string VehicleUsageCouldntEnteredMessage = "Vehicle Usage Couldn't be entered";
        public static string VehicleUsageSuccessfullyDeletedMessage = "Vehicle usage successfully deleted";
        public static string VehicleUsageCouldntDeletedMessage = "Vehicle usage couldn't deleted";
        public static string VehicleUsageSuccessfullyUpdatedMessage = "Vehicle Usage successfully updated";
        public static string VehicleUsageCouldntUpdatedMessage = "Vehicle Usage couldn't updated";
        public static string VehicleSuccessfullyAddedMessage = "Vehicle successfully added";
        public static string VehicleCouldntAddedMessage = "Vehicle couldn't added";
        public static string VehicleSuccessfullyDeletedMessage = "Vehicle successfully deleted!";
        public static string VehicleCouldntDeletedMessage = "Vehicle couldn't deleted";
        public static string VehicleSuccessfullyUpdatedMessage = "Vehicle successfully updated";
        public static string VehicleCouldntUpdatedMessage = "Vehicle couldn't updated";
        public static string RoleSuccessfullyAddedMessage = "Role successfully added";
        public static string RoleCouldntAddedMessage = "Role couldn't added";
        public static string UserNotFoundForRoleMessage = "User that you tried to change role was not found";
        public static string UserDatasNotValidMessage = "User datas aren't valid";
        public static string SuccessfullyLoggedInMessage = "Successfully logged-in !";
        public static string UserRoleAndTokenErrorMessage = "User's roles couldn't find and token couldn't create";
        public static string UserSuccessfullyCreatedMessage = "User successfully created";
        public static string TheCarAlreadyHadVehicleUsageError = "The car that you want to add a usage is already had it";

    }
}
