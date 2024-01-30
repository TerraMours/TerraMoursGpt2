import { defineMock } from 'vite-plugin-mock-dev-server';
import { routeModel, userModel } from '../model';

export default defineMock({
  url: '/mock/getUserRoutes',
  method: 'POST',
  body(req) {
    const { userId = undefined } = req.body;
    const routeHomeName: AuthRoute.LastDegreeRouteKey = 'dashboard_analysis';

    const role = userModel.find(item => item.userId === userId)?.roleId || 'user';

    const filterRoutes = routeModel[role];

    return {
      code: 200,
      message: 'ok',
      data: {
        routes: filterRoutes,
        home: routeHomeName
      }
    };
  }
});
