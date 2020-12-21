using System;
using System.Collections.Generic;
using Foundation;
using SignInUser.Common.Extensions;
using UIKit;

namespace SignInUser.Models
{
    public class TableSource : UITableViewSource
    {
        readonly List<User> _users;
        public TableSource(List<User> users)
        {
            _users = users;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(Constants.ReusableCellIdentifier);

            //Create new cell
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, Constants.ReusableCellIdentifier);

            User user = _users[indexPath.Row];
            cell.TextLabel.Text = $"{user.FirstName} {user.LastName}";
            cell.DetailTextLabel.Text = user.UserName;

            return cell;

        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _users.Count;
        }
    }
}
