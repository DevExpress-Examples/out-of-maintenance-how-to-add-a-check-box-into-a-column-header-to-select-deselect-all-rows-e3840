# How to add a check box into a column header to select / deselect all rows

`GridControl` allows you to show check boxes in column headers as follows.

* To edit a Boolean property or an [unbound column](https://docs.devexpress.com/WPF/6124/controls-and-libraries/data-grid/grid-view-data-layout/columns-and-card-fields/unbound-columns), set the [ShowCheckBoxInHeader](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.ColumnBase.ShowCheckBoxInHeader) property for your grid column to `true`.

```xml
<dxg:GridColumn FieldName="IsChecked" ShowCheckBoxInHeader="True" />
```

* To select multiple rows using check boxes, set [ShowCheckBoxSelectorColumn](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.ShowCheckBoxSelectorColumn) to `true`.

```xml
<dxg:GridControl ... SelectionMode="Row">
    ...
    <dxg:GridControl.View>
        <dxg:TableView ... ShowCheckBoxSelectorColumn="True" />
    </dxg:GridControl.View>
</dxg:GridControl>
``` 