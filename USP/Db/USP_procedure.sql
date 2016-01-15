CREATE PROCEDURE UP_ShowPage
@strGetFields varchar(MAX) = '*', --  Need to return the column, the default  *
@tblName   varchar(MAX),       --  Table name
@strWhere varchar(MAX) = '', --  Query criteria   ( Note  :  Do not add   where)
@strOrder varchar(MAX)='',      --  Sort of a field name, required
@strOrderType varchar(max)='ASC', --  By default, the sort of way  ASC
@PageIndex int = 1,           --  The page number, the default  1
@PageSize   int = 10 ,         --  By default, the page size  10
@RowCnt bigint = null
AS
declare @strSQL   varchar(MAX)
declare @TEST   varchar(MAX)
if @PageSize>0 and @PageIndex=0
begin
 set @PageIndex = 1
end
if @strWhere !=''
set @strWhere=' where '+@strWhere
set @strSQL=cast('SELECT * FROM (' as varchar(max))
        + cast(' SELECT convert(bigint,ROW_NUMBER() OVER (ORDER BY ' as varchar(max)) + @strOrder+cast(' ' as varchar(max))+ @strOrderType+cast(')) AS RowNo,'
        + 'convert(bigint,' + ISNULL(cast(@RowCnt as varchar(max)), 'COUNT(0) OVER ()') + ')' + ' as RowCnt'+','
        +' * ' as varchar(max)) +
        cast(' FROM ( ' as varchar(max))+ @strGetFields + cast(') AS a) AS SP ' as varchar(max))
if not(@PageIndex=0 and @PageSize=0)
begin
 set @strSQL= @strSQL+cast('WHERE RowNo BETWEEN ' as varchar(max)) + convert(varchar(max),(@PageIndex-1)*@PageSize+1) + cast(' AND ' as varchar(max)) + convert(varchar(max),@PageIndex*@PageSize)
end
exec (@strSQL)
GO

CREATE PROCEDURE UP_ShowArea
@PageIndex int = 1,
@PageSize int = 10,
@strOrder varchar(MAX) = '',
@strOrderType varchar(max) = 'ASC'
as
select * from(
    select convert(bigint, ROW_NUMBER() OVER(order by code asc)) as RowNo,
    convert(bigint, COUNT(0) OVER()) as RowCnt, *
    FROM(
        SELECT
        Parent,
        Code,
        Name,
        LocationX,
        LocationY,
        Reserve,
        Remark,
        CreateTime FROM SysArea
    ) AS a
)
AS temp WHERE RowNo BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize
go

CREATE PROCEDURE UP_AddMenu
@Name varchar(100),
@Icon varchar(100)
as
set @Icon= ISNULL(@Icon,'')
if not exists(select 1 from SysMenu where name=@Name and Parent=0)
begin
    insert into SysMenu(Direction, Parent,Name, Icon, Clazz, Area, Controller, Method, Parameter, Url, Creator,createTime)
    values(0,0,@Name,@Icon,'','','','','','',0,getdate())
end
go


CREATE PROCEDURE UP_AddMenuItem
@Parent varchar(100),
@Name varchar(100),
@Icon varchar(100),
@Class varchar(100),
@Area varchar(100),
@Controller varchar(200),
@Action varchar(100),
@Parameter varchar(1000),
@Url varchar(max)
as
declare @ParentID bigint
set @Icon= ISNULL(@Icon,'')
if @Parent=@Name
begin
    if not exists(select 1 from SysMenu where name=@Parent)
    begin
        insert into SysMenu(Direction,Parent,Name,Icon,Clazz,Area, Controller, method,Parameter,Url,Creator,CreateTime)
        values(0,0,@Parent,@Icon,'','','','','','',0,getdate())
    end
    return
end
if exists(select 1 from SysMenu where name=@Parent)
begin
    select @ParentID=ID from SysMenu where name=@Parent
    if exists(select 1 from SysMenu where Name=@Name and Parent=@ParentID)
    begin
        return
    end
end
else
begin
    insert into SysMenu(Direction,Parent,Name,Icon,Clazz,Area, Controller,Method,Parameter,Url,Creator,CreateTime)
    values(0,0,@Parent,'','','','','','','',0,getdate())
    select @ParentID=@@IDENTITY
end
insert into SysMenu(Direction,Parent,Name,Icon,Clazz,Area,Controller,Method,Parameter,Url,Creator,CreateTime)
    values(0,@ParentID,@Name,@Icon,@Class,@Area,@Controller,@Action,@Parameter,@Url,0,getdate())
go

CREATE PROCEDURE UP_AddPrivilege
@Menu varchar(100),
@Parent varchar(100),
@Name varchar(100),
@Class varchar(100),
@Area varchar(100),
@Controller varchar(200),
@Action varchar(100),
@Parameter varchar(1000),
@Url varchar(1000)
as
declare @MenuID bigint
declare @ParentID bigint
select @MenuID=ID from SysMenu where Name=@Menu
select @MenuID=IsNUll(@MenuID,0)
select @ParentID=ID from SysPrivilege where Name=@Parent
select @ParentID=IsNUll(@ParentID,0)
select @Parent=IsNUll(@Parent,'')
if @Parent!=''
begin
	if exists(select 1 from SysPrivilege where Name=@Parent)
	begin
	    select @ParentID=ID from SysPrivilege where Name=@Parent
	    if exists(select 1 from SysPrivilege where Name=@Name and Parent=@ParentID)
	    begin
	        return
	    end
	end
	else
	begin
	    insert into SysPrivilege(Menu,Parent,Name,Clazz,Area,Controller,Method,Parameter,Url,Creator,CreateTime)
	    values(@MenuID,@ParentID,@Parent,'','','','','','',0,getdate())
	    select @ParentID=@@IDENTITY
	end
end
if not exists(select 1 from SysPrivilege where Name=@Name and Menu=@MenuID and Parent=@ParentID)
begin
	insert into SysPrivilege(Menu,Parent,Name,Clazz,Area,Controller,Method,Parameter,Url,Creator,CreateTime)
		values(@MenuID,@ParentID,@Name,@Class,@Area,@Controller,@Action,@Parameter,@Url,0,getdate())
end
go

CREATE PROCEDURE UP_Login
@LoginName varchar(100),
@Password varchar(32),
@Session varchar(100),
@IP varchar(100)
as
if exists(select 1 from  SysOperator where LoginName = @LoginName and [Password]=@Password and Status=0)
begin
    update SysOperator set LoginCount = LoginCount +1 ,LoginTime=getdate(), LoginIP=@IP, Session=@Session
    where LoginName = @LoginName
    and [Password]=@Password
    and Status=0

    select * from SysOperator
    where LoginName = @LoginName
    and [Password]=@Password
    and Status=0
end
else
begin
    update SysOperator set LoginErrorCount = LoginErrorCount +1 ,LoginTime=getdate(), LoginIP=@IP
    where LoginName = @LoginName

    select * from SysOperator where 1=2
end
go

CREATE PROCEDURE UP_GetOperatorMenu
@Operator bigint
as
    select a.* from SysMenu a,SysRoleMenu b,SysRoleOperator c,SysOperator d
    where a.ID=b.Menu
    and b.Role=c.Role
    and c.Operator=d.ID
    and d.ID = @Operator
    and d.Status=0
go

CREATE PROCEDURE UP_GetOperatorPrivilege
@Operator bigint
as
    select a.* from SysPrivilege a,SysRolePrivilege b,SysRoleOperator c,SysOperator d
    where a.ID=b.Privilege
    and b.Role=c.Role
    and c.Operator=d.ID
    and d.ID = @Operator
    and d.Status=0
go

CREATE Procedure UP_CheckSSO
@Operator bigint,
@Session varchar(100)
as
Declare @result varchar(1000)
Declare @VirtualIntegral bigint
Declare @RealIntegral bigint
Declare @Balance decimal
Declare @FrozenBalance decimal
Declare @IncomingBalance decimal
Declare @Grade int
Declare @Star int
select @VirtualIntegral= VirtualIntegral,@RealIntegral = RealIntegral,@Balance=Balance,@FrozenBalance=FrozenBalance,@IncomingBalance=IncomingBalance,@Grade=Grade,@Star=Star
from SysOperator with(nolock)
where ID = @Operator
if exists(select 1 from SysOperator with(nolock) where ID = @Operator and Session = @Session)
begin
    select @result = 'true|' + convert(varchar(100),@VirtualIntegral) + '|' + convert(varchar(100),@RealIntegral) + '|' + convert(varchar(100),@Balance)+ '|' + convert(varchar(100),@FrozenBalance)+ '|' + convert(varchar(100),@IncomingBalance)+ '|' + convert(varchar(100),@Grade)+ '|' + convert(varchar(100),@Star)
end
else
begin
    select @result = 'false|' + convert(varchar(100),@VirtualIntegral) + '|' + convert(varchar(100),@RealIntegral) + '|' + convert(varchar(100),@Balance)+ '|' + convert(varchar(100),@FrozenBalance)+ '|' + convert(varchar(100),@IncomingBalance)+ '|' + convert(varchar(100),@Grade)+ '|' + convert(varchar(100),@Star)
end
select @result
go


--根据角色获取菜单
CREATE PROCEDURE UP_GetRoleMenu
@Role bigint
as
    select a.* from SysMenu a,SysRoleMenu b
    where a.ID=b.Menu
    and b.Role=@Role
go


--根据角色获取权限
CREATE PROCEDURE UP_GetRolePrivilege
@Role bigint
as
    select a.* from SysPrivilege a,SysRolePrivilege b
    where a.ID=b.Privilege
    and b.Role=@Role
go


--添加角色
CREATE PROCEDURE UP_AddRole
@Corp bigint,
@Name varchar(100),
@Type bit,
@Remark varchar(250),
@Creator bigint,
@Menus varchar(max),
@Privileges varchar(max)
as
declare @RoleID bigint 

if exists(select 1 from SysRole where Name=@Name and Corp=@Corp)
begin
	select null
end
else 
begin
	declare @tran_error int;
	set @tran_error = 0;
	begin tran
	
	begin try 
	
	--添加角色
	insert into SysRole(Corp,Name,Type,Remark,Creator,Auditor) values(@Corp,@Name,@Type,@Remark,@Creator,@Creator)
	select @RoleID=@@IDENTITY
	
	--添加菜单和权限
	declare @sqlmenu  varchar(max),@sqlprivilege varchar(max)
	set @sqlmenu='insert into SysRoleMenu(Role,Menu,Creator,Auditor) select '+cast(@RoleID as varchar(max))+',ID,'+cast(@Creator as varchar(max))+','+cast(@Creator as varchar(max))+' from SysMenu where ID in ('+@Menus+')'
	exec (@sqlmenu)
	
	set @sqlmenu='insert into SysRolePrivilege(Role,Menu,Creator,Auditor) select '+cast(@RoleID as varchar(max))+',ID,'+cast(@Creator as varchar(max))+','+cast(@Creator as varchar(max))+' from SysPrivilege where ID in ('+@Privileges+')'
	exec (@sqlprivilege)
	
	end try
	
	begin catch
	set @tran_error = @tran_error + 1
	end catch 
	
	if(@tran_error > 0)
    begin        
        rollback tran;--执行出错，回滚事务
        select null
    end
	else
    begin        
        commit tran;--没有异常，提交事务
        select @RoleID
    end	
end
go

--修改角色
Create PROCEDURE UP_EditRole
@RoleID bigint,
@Name varchar(100),
@Remark varchar(250),
@Creator bigint,
@Menus varchar(max),
@Privileges varchar(max)
as

--角色不存在返回空
if not exists(select 1 from SysRole where ID=@RoleID)
begin
	select null
end
else 
begin
	declare @tran_error int;
	set @tran_error = 0;
	begin tran
	
	begin try 
	update SysRole set Name=@Name,Remark=@Remark,Creator=@Creator,CreateTime=getdate() where ID=@RoleID

	--菜单处理
	declare @sqlmenudelete varchar(max),@sqlmenuadd varchar(max)

	set @sqlmenudelete='delete from SysRoleMenu where Role='+cast(@RoleID as varchar(max))+' and Menu not in ('+@Menus+')'
	exec (@sqlmenudelete)

	set @sqlmenuadd='insert into SysRoleMenu(Role,Menu,Creator,Auditor) (select '+cast(@RoleID as varchar(max))+',ID,'+cast(@Creator as varchar(max))+','+cast(@Creator as varchar(max))+' from SysMenu where ID in ('+@Menus+') and ID not in (select menu from SysRoleMenu where Role='+cast(@RoleID as varchar(max))+'))'
	exec (@sqlmenuadd)

	--权限处理
	declare @sqlprivilegedelete varchar(max),@sqlprivilegeadd varchar(max)
	set @sqlprivilegedelete='delete from SysRolePrivilege where Role='+cast(@RoleID as varchar(max))+' and Privilege not in ('+@Privileges+')'
	exec (@sqlprivilegedelete)

	set @sqlprivilegeadd='insert into SysRolePrivilege(Role,Privilege,Creator,Auditor) (select '+cast(@RoleID as varchar(max))+',ID,'+cast(@Creator as varchar(max))+','+cast(@Creator as varchar(max))+' from SysPrivilege where ID in ('+@Privileges+') and ID not in (select Privilege from SysRolePrivilege where Role='+cast(@RoleID as varchar(max))+'))'
	exec (@sqlprivilegeadd)
	
	end try
	
	begin catch
	set @tran_error = @tran_error + 1
	end catch 
	
	if(@tran_error > 0)
    begin        
        rollback tran;--执行出错，回滚事务
        select null
    end
	else
    begin        
        commit tran;--没有异常，提交事务
		select @RoleID	
    end   	
end
go

--根据用户获取角色
CREATE PROCEDURE UP_GetRoleByOperator
@Operator bigint
as
    select a.* from SysRole a,SysRoleOperator b,SysOperator c
    where a.ID=b.Role
    and b.Operator=c.ID
    and c.ID = @Operator
    and c.Status=0
go
