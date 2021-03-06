alter table SysCorpVocation add constraint FK_SysCorpVocation_Parent foreign key(Parent) references SysCorpVocation(ID)
alter table SysCorp add constraint FK_SysCorp_Vocation foreign key(Vocation) references SysCorpVocation(ID)
alter table SysRoleOperator add constraint FK_SysRoleOperator_Role foreign key(Role) references SysRole(ID)
alter table SysRoleMenu add constraint FK_SysRoleMenu_Role foreign key(Role) references SysRole(ID)
alter table SysRolePrivilege add constraint FK_SysRolePrivilege_Role foreign key(Role) references SysRole(ID)
alter table SysCorp add constraint FK_SysCorp_Status foreign key(Status) references SysCorpStatus(ID)
alter table SysMenuTemplate add constraint FK_SysMenuTemplate_CorpType foreign key(CorpType) references SysCorpType(ID)
alter table SysPrivilegeTemplate add constraint FK_SysPrivilegeTemplate_CorpType foreign key(CorpType) references SysCorpType(ID)
alter table SysCorp add constraint FK_SysCorp_Type foreign key(Type) references SysCorpType(ID)
alter table SysCorp add constraint FK_SysCorp_Grade foreign key(Grade) references SysCorpGrade(ID)
alter table SysCorp add constraint FK_SysCorp_FeeType foreign key(FeeType) references SysCorpFeeType(ID)
alter table OpenPlatformDefaultMsg add constraint FK_OpenPlatformDefaultMsg_Corp foreign key(Corp) references SysCorp(ID)
alter table SysRole add constraint FK_SysRole_Corp foreign key(Corp) references SysCorp(ID)
alter table SysCorp add constraint FK_SysCorp_Parent foreign key(Parent) references SysCorp(ID)
alter table OpenPlatform add constraint FK_OpenPlatform_Corp foreign key(Corp) references SysCorp(ID)
alter table OpenPlatformAccessToken add constraint FK_OpenPlatformAccessToken_Corp foreign key(Corp) references SysCorp(ID)
alter table OpenPlatformSubscriber add constraint FK_OpenPlatformSubscriber_Corp foreign key(Corp) references SysCorp(ID)
alter table OpenPlatformMenu add constraint FK_OpenPlatformMenu_Corp foreign key(Corp) references SysCorp(ID)
alter table OpenPlatformMsg add constraint FK_OpenPlateform_Corp foreign key(Corp) references SysCorp(ID)
alter table OpenPlatformMsgKey add constraint FK_OpenPlatformMsgKey_Corp foreign key(Corp) references SysCorp(ID)
alter table OpenPlatformMO add constraint FK_OpenPlatformMO_PlatformType foreign key(PlatformType) references OpenPlatformType(Type)
alter table OpenPlatformMT add constraint FK_OpenPlatformMPlatformType foreign key(PlatformType) references OpenPlatformType(Type)
alter table OpenPlatform add constraint FK_OpenPlatform_PlatformType foreign key(PlatformType) references OpenPlatformType(Type)
alter table OpenPlatformUserInfo add constraint FK_OpenPlatformUserInfo_PlatformType foreign key(PlatformType) references OpenPlatformType(Type)
alter table OpenPlatformAccessToken add constraint FK_OpenPlatformAccessToken_PlatformType foreign key(PlatformType) references OpenPlatformType(Type)
alter table OpenPlatformSubscriber add constraint FK_OpenPlatformSubscriber_PlatformType foreign key(PlatformType) references OpenPlatformType(Type)
alter table OpenPlatformMenuType add constraint FK_OpenPlatformMenuType_PlatformType foreign key(PlatformType) references OpenPlatformType(Type)
alter table OpenPlatformMenu add constraint FK_OpenPlatformMenu_PlatformType foreign key(PlatformType) references OpenPlatformType(Type)
alter table OpenPlatformMsgType add constraint FK_OpenPlatformMsgType_PlatformType foreign key(PlatformType) references OpenPlatformType(Type)
alter table OpenPlatformMsg add constraint FK_OpenPlateform_PlatformType foreign key(PlatformType) references OpenPlatformType(Type)
alter table SysOperator add constraint FK_SysOperator_Skin foreign key(Skin) references SysSkin(ID)
alter table SysOperator add constraint FK_SysOperator_Status foreign key(Status) references SysOperatorStatus(ID)
alter table SysOperator add constraint FK_SysOperator_Grade foreign key(Grade) references SysOperatorGrade(ID)
alter table SysOperator add constraint FK_SysOperator_Star foreign key(Star) references SysOperatorStar(ID)
alter table OpenPlatformDefaultMsg add constraint FK_OpenPlatformDefaultMsg_Creator foreign key(Creator) references SysOperator(ID)
alter table SysPrivilege add constraint FK_SysPrivilege_Creator foreign key(Creator) references SysOperator(ID)
alter table SysRole add constraint FK_SysRole_Creator foreign key(Creator) references SysOperator(ID)
alter table SysRoleOperator add constraint FK_SysRoleOperator_Operator foreign key(Operator) references SysOperator(ID)
alter table SysRoleOperator add constraint FK_SysRoleOperator_Creator foreign key(Creator) references SysOperator(ID)
alter table SysRoleMenu add constraint FK_SysRoleMenu_Creator foreign key(Creator) references SysOperator(ID)
alter table SysCorpVocation add constraint FK_SysCorpVocation_Creator foreign key(Creator) references SysOperator(ID)
alter table SysCorpStatus add constraint FK_SysCorpStatus_Creator foreign key(Creator) references SysOperator(ID)
alter table SysCorpType add constraint FK_SysCorpType_Creator foreign key(Creator) references SysOperator(ID)
alter table SysRolePrivilege add constraint FK_SysRolePrivilege_Creator foreign key(Creator) references SysOperator(ID)
alter table SysCorpGrade add constraint FK_SysCorpGrade_Creator foreign key(Creator) references SysOperator(ID)
alter table SysCorpFeeType add constraint FK_SysCorpFeeType_Creator foreign key(Creator) references SysOperator(ID)
alter table SysCorp add constraint FK_SysCorp_Creator foreign key(Creator) references SysOperator(ID)
alter table SysOperator add constraint FK_SysOperator_Creator foreign key(Creator) references SysOperator(ID)
alter table SysMenuTemplate add constraint FK_SysMenuTemplate_Creator foreign key(Creator) references SysOperator(ID)
alter table SysPrivilegeTemplate add constraint FK_SysPrivilegeTemplate_Creator foreign key(Creator) references SysOperator(ID)
alter table OpenPlatform add constraint FK_OpenPlatform_Creator foreign key(Creator) references SysOperator(ID)
alter table OpenPlatformMenu add constraint FK_OpenPlatformMenu_Creator foreign key(Creator) references SysOperator(ID)
alter table IdCardImg add constraint FK_IdCardImg_Creator foreign key(Creator) references SysOperator(ID)
alter table SysLoginLog add constraint FK_SysLoginLog_Operator foreign key(Operator) references SysOperator(ID)
alter table OpenPlatformMsg add constraint FK_OpenPlateform_Creator foreign key(Creator) references SysOperator(ID)
alter table SysMenu add constraint FK_SysMenu_Creator foreign key(Creator) references SysOperator(ID)
alter table OpenPlatformMsgKey add constraint FK_OpenPlatformMsgKey_Creator foreign key(Creator) references SysOperator(ID)
alter table OpenPlatformMenu add constraint FK_OpenPlatformMenu_Type foreign key(Type) references OpenPlatformMenuType(Type)
alter table OpenPlatformMenu add constraint FK_OpenPlatformMenu_Parent foreign key(Parent) references OpenPlatformMenu(ID)
alter table OpenPlatformMO add constraint FK_OpenPlatformMO_MsgType foreign key(MsgType) references OpenPlatformMsgType(Type)
alter table OpenPlatformMT add constraint FK_OpenPlatformMMsgType foreign key(MsgType) references OpenPlatformMsgType(Type)
alter table OpenPlatformMsgChildren add constraint FK_OpenPlatformMsgChildren_Parent foreign key(Parent) references OpenPlatformMsg(ID)
alter table OpenPlatformMsgChildren add constraint FK_OpenPlatformMsgChildren_Child foreign key(Child) references OpenPlatformMsg(ID)
alter table OpenPlatformMsgKey add constraint FK_OpenPlatformMsgKey_msg foreign key(Msg) references OpenPlatformMsg(ID)
alter table SysPrivilege add constraint FK_SysPrivilege_Menu foreign key(Menu) references SysMenu(ID)
alter table SysRoleMenu add constraint FK_SysRoleMenu_Menu foreign key(Menu) references SysMenu(ID)
alter table SysMenu add constraint FK_SysMenu_parent foreign key(Parent) references SysMenu(ID)
alter table SysMenuTemplate add constraint FK_SysMenuTemplate_Menu foreign key(Menu) references SysMenu(ID)
alter table SysRolePrivilege add constraint FK_SysRolePrivilege_Privilege foreign key(Privilege) references SysPrivilege(ID)
alter table SysPrivilege add constraint FK_SysPrivilege_Parent foreign key(Parent) references SysPrivilege(ID)
alter table SysPrivilegeTemplate add constraint FK_SysPrivilegeTemplate_Privilege foreign key(Privilege) references SysPrivilege(ID)
alter table SysArea add constraint FK_SysArea_Parent foreign key(Parent) references SysArea(Code)