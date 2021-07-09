using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product Added";

        public static string ProductNameInvalid = "Invalid Product Name";

        public static string MaintenanceTime = "System in maintenance";

        public static string ProductsListed = "Products listed";

        public static string ProductMaximumNumberOfCategoryReachedError = "Maximum number for specified category has been reached!";

        public  static string ProductNameExistsError = "Product Name Already Exists!";

        public  static string CategoryLimitExceeded = "Maximum number for category has been reached!";

        public  static string AuthorizationDenied = "You do not have an authorization for this process!";
        internal static string UserRegistered;
        internal static User UserNotFound;
        internal static User PasswordError;
        internal static string SuccessfulLogin;
        internal static string UserAlreadyExists;
        internal static string AccessTokenCreated;
    }
}
