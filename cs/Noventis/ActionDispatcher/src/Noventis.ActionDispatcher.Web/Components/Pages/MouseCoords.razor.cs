using Microsoft.AspNetCore.Components.Web;

namespace Noventis.ActionDispatcher.Web.Components.Pages
{
    public partial class MouseCoords(ICoreComponent core)
    {
        private int hoverX = 0;
        private int hoverY = 0;
        private int mouseDownX = -1;
        private int mouseDownY = -1;
        private int clickX = -1;
        private int clickY = -1;
        private int releaseX = -1;
        private int releaseY = -1;

        private void HandleMouseMove(MouseEventArgs e)
        {
            // Get the relative coordinates within the element
            hoverX = (int)e.OffsetX;
            hoverY = (int)e.OffsetY;
        }

        private void HandleMouseDown(MouseEventArgs e)
        {
            // Capture the mouse down coordinates
            mouseDownX = (int)e.OffsetX;
            mouseDownY = (int)e.OffsetY;
        }

        private void HandleClick(MouseEventArgs e)
        {
            // Capture the click coordinates
            clickX = (int)e.OffsetX;
            clickY = (int)e.OffsetY;
        }

        private void HandleMouseUp(MouseEventArgs e)
        {
            // Capture the mouse release coordinates
            releaseX = (int)e.OffsetX;
            releaseY = (int)e.OffsetY;

            //Difference between Mouse Down and Release
            int diffX = releaseX - mouseDownX;
            int diffY = releaseY - mouseDownY;

            core.MouseMoveRelative(diffX, diffY);
        }

        private void HandleMouseLeave(MouseEventArgs e)
        {
            // Optional: Reset hover coordinates when mouse leaves
            hoverX = 0;
            hoverY = 0;
        }

        private void MouseClickAction()
        {
            core.MouseClick();
        }

        //Right Click Action
        private void MouseRightClickAction()
        {
            core.MouseRightClick();
        }
    }
}