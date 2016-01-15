(function () {
    usp.namespace("usp.sysytem.role");
    usp.sysytem.role.initRoleGrid = function (id, url) {
        $(id).datagrid({
            url: url,
            title: '角色管理',
            iconCls: 'icon-clientService',
            height: 500,
            nowrap: false,
            striped: true,
            idField: "ID",
            pagination: true,
            rownumbers: true,
            singleSelect: true,
            columns: [
                [
                    {
                        field: 'Name',
                        title: '角色名称',
                        width: 300
                    }, {
                        field: 'Remark',
                        title: '备注',
                        width: 300
                    }, {
                        field: 'CreateTime',
                        title: '创建时间',
                        width: 300,
                        formatter: function (val) {
                            return usp.ParseUTCDate(val);
                        }
                    }
                ]
            ],
            toolbar: [
            {
                iconCls: 'icon-add',
                text: '新增',
                handler: function () {
                    location.href = '/System/Role/addRole';
                }
            }, '-', {
                iconCls: 'icon-edit',
                text: '修改',
                handler: function () {
                    var row = $(id).datagrid('getSelected');
                    if (row == null) {
                        $.messager.alert('提醒', '请选择要修改的角色！', 'warning');
                    } else {
                        if (row.Type == true) {
                            $.messager.alert('提醒', '您不能修改管理员角色！', 'warning');
                        } else {
                            location.href = '/System/Role/EditRole/' + row.ID;
                        }
                    }
                }
            }]
        });
    }

    usp.sysytem.role.initMenuPrivilegeTree = function (treeid, url, roleid, menuid, privilegeid) {
        $(treeid).tree({
            url: url,
            queryParams: { role: $(roleid).val() },
            checkbox: true,
            lines: true,
            onCheck: function (node) {
                usp.sysytem.role.setVaule(treeid, menuid, privilegeid);
            },
            onLoadSuccess: function () {
                usp.sysytem.role.setVaule(treeid, menuid, privilegeid);
            }
        });
    };

    usp.sysytem.role.setVaule = function (treeid, menuid, privilegeid) {
        var checknodes = $(treeid).tree('getChecked', ['checked', 'indeterminate']); //选择和半选择的节点
        var menus = '';
        var privileges = '';
        for (i = 0; i < checknodes.length; i++) {
            var type = checknodes[i].attributes.type;
            var id = checknodes[i].id;
            if (type == 1) {
                if (menus != '') menus += ',';
                menus += id;
            } else {
                if (privileges != '') privileges += ',';
                privileges += id;
            }
        };
        $(menuid).val(menus);
        $(privilegeid).val(privileges);
    }

    usp.sysytem.role.checkprivilieges = function (menuid) {
        if ($(menuid).val() == '') {
            $.messager.alert('提醒', '请选择权限！', 'warning');
            return false;
        }
        return true;
    }

    usp.sysytem.role.checkRoleName = function (nameid, errorid, roleid, url) {
        $(nameid).blur(function () {
            var name = $(this).val();
            if (name != '') {
                $.post(url, { name: name, role: $(roleid).val() }, function (d) {
                    if (d == '0') {
                        $(errorid).text('角色名已存在');
                        $(errorid).show();
                    } else if (d == '1') {
                        $(errorid).text('');
                        $(errorid).hide();
                    } else if (d == '2') {
                        $(errorid).text('请输入角色名称');
                        $(errorid).show();
                    }
                }, 'text');
            }
        });
    };
})(this);