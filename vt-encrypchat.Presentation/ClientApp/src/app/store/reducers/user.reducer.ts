import {Action, ActionReducer, createReducer, on} from '@ngrx/store';
import {
  getUserSettings,
  getUserSettingsFail,
  getUserSettingsSuccess,
  getUsersSuccess, loadUserPublicKey,
  setUserSettings,
  setUserSettingsFail,
  setUserSettingsSuccess,
} from '../actions';
import {User} from '../../models/user';

export interface State {
  displayName: string;
  publicGPGKey: string;
  userSettingsLoaded: boolean;
  users: User[];
}

export const initialState: State = {
  displayName: '',
  publicGPGKey: '',
  userSettingsLoaded: false,
  users: [],
};

const reducer: ActionReducer<State> = createReducer(
  initialState,
  on(getUserSettings, (state) => ({
    ...state,
  })),
  on(getUserSettingsSuccess, (state, { payload }) => ({
    ...state,
    displayName: payload.displayName,
    publicGPGKey: payload.gpgKey,
    userSettingsLoaded: true,
  })),
  on(getUserSettingsFail, (state) => ({
    ...state,
    displayName: '',
    publicGPGKey: '',
    userSettingsLoaded: false,
  })),
  on(setUserSettings, (state) => ({
    ...state,
  })),
  on(setUserSettingsSuccess, (state, { payload }) => ({
    ...state,
    displayName: payload.displayName,
    publicGPGKey: payload.gpgKey,
  })),
  on(setUserSettingsFail, (state) => ({
    ...state,
  })),
  on(loadUserPublicKey, (state, { payload }) => ({
    ...state,
    publicGPGKey: payload.publicKey
  })),
  on(getUsersSuccess, (state, { payload }) => ({
    ...state,
    users: payload.users,
  })),
);

export function userReducer(state: State | undefined, action: Action): State {
  return reducer(state, action);
}
