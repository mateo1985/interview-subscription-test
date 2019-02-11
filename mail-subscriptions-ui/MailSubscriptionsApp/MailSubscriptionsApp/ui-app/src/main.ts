import 'hammerjs';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './modules/app/app.module';
import { environment } from './environments/environment';

console.log("%cInterview application logs", "color: red;font-size: 20px;");
console.log("%cPlease observe logs", "color: red;font-size: 15px;");
console.log("%cSome of these logs should not be visible for the user on production", "color: red;font-size: 15px;");

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
