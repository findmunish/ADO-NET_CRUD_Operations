/****** Object:  Database:  [AdoDotNetEmpDeptDb]   ******/

/****** Object:  Table [dbo].[Departments]    ******/

/****** Object:  StoredProcedure [dbo].[usp_getDepartments]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure usp_getDepartments
as
Begin
    select *
    from Departments
    order by Id
End

/****** Object:  StoredProcedure [dbo].[usp_getDepartmentById]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure usp_getDepartmentById
@DeptId int
as
Begin
    select *
    from Departments
     where DeptId=@DeptId 
    order by Id 
End

/****** Object:  StoredProcedure [dbo].[usp_addDepartment]    PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure usp_addDepartment
@Name VARCHAR(50)
as
Begin
    Insert into Departments (Name)
    Values (@Name)
End
GO

/****** Object:  StoredProcedure [dbo].[[usp_updateDepartment]] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure usp_updateDepartment
(
    @DeptId INTEGER,
    @Name VARCHAR(50)
)
as
begin
   Update Departments
   set Name=@Name
   where DeptId=@DeptId
End
GO

/****** Object:  StoredProcedure [dbo].[[usp_deleteDepartment]]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure usp_deleteDepartment
(
   @DeptId int
)
as
begin
   Delete from Departments where DeptId=@DeptId
End
GO


/****** Object:  Table [dbo].[Employees]    ******/

/****** Object:  StoredProcedure [dbo].[usp_getEmployees]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure usp_getEmployees
as
Begin
    select *
    from Employees
    order by Id
End

/****** Object:  StoredProcedure [dbo].[usp_getDepartmentById]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure usp_getEmployeeById
@EmpId int
as
Begin
    select *
    from Employees
     where EmpId=@EmpId
    order by Id 
End

/****** Object:  StoredProcedure [dbo].[usp_addEmployee]    PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure usp_addEmployee
@Name VARCHAR(50)
as
Begin
    Insert into Employees (Name)
    Values (@Name)
End
GO

/****** Object:  StoredProcedure [dbo].[[usp_updateEmployee]] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure usp_updateEmployee
(
    @EmpId INTEGER,
    @Name VARCHAR(50)
)
as
begin
   Update Employees
   set Name=@Name
   where EmpId=@EmpId
End
GO

/****** Object:  StoredProcedure [dbo].[[usp_deleteEmployee]]    ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[usp_deleteEmployee]
(
   @EmpId int
)
as
begin
   Delete from Employees where EmpId=@EmpId
End
GO
