import {Action, ActionReducer, createReducer, on} from '@ngrx/store';
import {
  getUserSettings, getUserSettingsFail, getUserSettingsSuccess, setUserSettings, setUserSettingsFail, setUserSettingsSuccess,
} from '../actions';

export interface State {
  displayName: string;
  publicGPGKey: string;
}

export const initialState: State = {
  displayName: '',
  publicGPGKey: '',
};

const reducer: ActionReducer<State> = createReducer(
  initialState,
  on(getUserSettings, (state) => ({
    ...state,
  })),
  on(getUserSettingsSuccess, (state, { payload }) => ({
    ...state,
    displayName: payload.displayName,
    publicGPGKey: payload.gpgKey
  })),
  on(getUserSettingsFail, (state) => ({
    ...state,
    displayName: '',
    publicGPGKey: '',
  })),
  on(setUserSettings, (state) => ({
    ...state,
  })),
  on(setUserSettingsSuccess, (state) => ({
    ...state,
    displayName: state.displayName,
    publicGPGKey: state.publicGPGKey,
  })),
  on(setUserSettingsFail, (state) => ({
    ...state,
  })),
);

export function userReducer(state: State | undefined, action: Action): State {
  return reducer(state, action);
}
