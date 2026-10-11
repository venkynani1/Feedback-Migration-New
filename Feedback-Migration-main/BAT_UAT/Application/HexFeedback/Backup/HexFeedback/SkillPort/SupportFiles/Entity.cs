#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
#endregion
/// <summary>
/// Summary description for Entity
/// </summary>
public class Entity
{
    #region "SkillPortData"
    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    /// <value>
    /// The name of the role.
    /// </value>
    public string SkillPortData { get; set; }
    #endregion

    #region "EmpID"
    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    /// <value>
    /// The name of the role.
    /// </value>
    public string EmpID { get; set; }
    #endregion

    #region Month
    /// <summary>
    /// Gets or sets the name of the Month.
    /// </summary>
    /// <value>
    /// The name of the Month.
    /// </value>
    public string Month { get; set; } 
    #endregion

    #region Year
    /// <summary>
    /// Gets or sets the name of the Year.
    /// </summary>
    /// <value>
    /// The name of the Year.
    /// </value>
    public string Year { get; set; } 
    #endregion

    #region FilePath
    /// <summary>
    /// Gets or sets the name of the FilePath.
    /// </summary>
    /// <value>
    /// The name of the FilePath.
    /// </value>
    public string FilePath { get; set; }
    #endregion


    #region "intReturnValue"
    /// <summary>
    /// Get or sets the return value from stored procedure.
    /// </summary>
    public int intReturnValue = 0;
    #endregion
 
}