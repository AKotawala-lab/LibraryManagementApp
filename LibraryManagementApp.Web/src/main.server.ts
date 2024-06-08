import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { config } from './app/app.config.server';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';

//const bootstrap = () => bootstrapApplication(AppComponent, config);
const bootstrap = () => {
    return platformBrowserDynamic().bootstrapModule(AppModule)
      .catch(err => console.error(err));
  };

export default bootstrap;
