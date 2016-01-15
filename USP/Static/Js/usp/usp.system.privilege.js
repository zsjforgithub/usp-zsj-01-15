(function () {
    usp.namespace("usp.sysytem.privilege");
    usp.sysytem.privilege.initGrid = function (id, url) {
        $(id).datagrid({
            url: url,
            title: '权限数据',
            iconCls: 'icon-clientService',
            fit: true,
            nowrap: false,
            striped: true,
            columns: [
                [{
                    field: 'Name',
                    title: '名称',
                    width: 300
                }, {
                    field: 'MenuName',
                    title: '菜单',
                    width: 300
                }, {
                    field: 'Url',
                    title: '地址',
                    width: 300
                }]
            ],
            pagination: true,
            rownumbers: true,
            singleSelect: true,
            toolbar: [
            {
                iconCls: 'icon-add',
                text: '新增',
                handler: function () {
                    location.href = '/System/System/AddPrivilege';
                }
            }, '-', {
                iconCls: 'icon-edit',
                text: '修改',
                handler: function () {
                    var row = $(id).datagrid('getSelected');
                    if (row == null) {
                        $.messager.alert('提醒', '请选择要修改的权限！', 'warning');
                    } else {
                        location.href = '/System/System/EditPrivilege/' + row.ID;
                    }
                }
            }]
        });
    }

    usp.sysytem.privilege.initMenuTree = function (treeid, url, hdmenu) {
        $(treeid).tree({
            url: url,
            lines: true,
            queryParams: { menu: $(hdmenu).val() },
            onSelect: function (node) {
                $(hdmenu).val(node.id);
            },
            onLoadSuccess: function () {
                //修改时选中菜单并展开到节点
                var menuid = $(hdmenu).val();
                if (menuid != '0' && menuid != '') {
                    var obj = $(treeid).tree('find', menuid);
                    if (obj) {
                        $(treeid).tree('select', obj.target);
                        $(treeid).tree("expandTo", obj.target);
                    }
                }
            }
        });
    };

    usp.sysytem.privilege.checkmenu = function (menuid, errorid) {
        if ($(menuid).val() == '' || $(menuid).val() == '0') {
            $(errorid).text('请选择菜单');
            $(errorid).show();
            return false;
        }
        $(errorid).text('');
        $(errorid).show();
        return true;
    };

    usp.sysytem.privilege.checkPrivilegeName = function (nameid, errorid, menuid, url) {
        $(nameid).blur(function () {
            var name = $(this).val();
            if (name != '') {
                $.post(url, { name: name, menu: $(menuid).val() }, function (d) {
                    if (d == '0') {
                        $(errorid).text('权限已存在');
                        $(errorid).show();
                    } else if (d == '1') {
                        $(errorid).text('');
                        $(errorid).hide();
                    } else if (d == '2') {
                        $(errorid).text('请输入权限名称');
                        $(errorid).show();
                    }
                }, 'text');
            }
        });
    };
})(this);