using DevExpress.Maui.CollectionView;
using DevExpress.Maui.Core;

namespace MvvmDemo.Modules.UIServiceDemo;

public interface ICollectionViewUIService {
    void ScrollToEnd();
}
public class CollectionViewUIService : UIServiceBase<DXCollectionView>, ICollectionViewUIService {
    public void ScrollToEnd() {
        AssociatedObject?.ScrollTo(AssociatedObject.VisibleItemCount, DXScrollToPosition.End);
    }
}