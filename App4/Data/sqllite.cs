using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App4.Data
{
    public interface sqllite
    {
        SQLiteConnection GetConnection();
    }
}
