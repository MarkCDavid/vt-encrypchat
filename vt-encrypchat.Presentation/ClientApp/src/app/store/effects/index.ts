import { AuthEffects } from './auth.effects';
import { RoutingEffects } from './routing.effects';
import { UserEffects } from './user.effects';
import { ToastsEffects } from './toasts.effects';
import {MessagesEffects} from "./messages.effects";

export const effects = [AuthEffects, RoutingEffects, UserEffects, ToastsEffects, MessagesEffects];
