interface UserModel extends Auth.UserInfo {
  token: string;
  refreshToken: string;
  password: string;
}

export const userModel: UserModel[] = [
  {
    token: '__TOKEN_SOYBEAN__',
    refreshToken: '__REFRESH_TOKEN_SOYBEAN__',
    userId: 0,
    userName: 'Soybean',
    roleId: 'super',
    password: 'soybean123',
	headImageUrl: null, // 头像url
	balance: 0 // 用户余额
  },
  {
    token: '__TOKEN_SUPER__',
    refreshToken: '__REFRESH_TOKEN_SUPER__',
    userId: 1,
    userName: 'Super',
    roleId: 'super',
    password: 'super123',
		headImageUrl: null, // 头像url
		balance: 0 // 用户余额
  },
  {
    token: '__TOKEN_ADMIN__',
    refreshToken: '__REFRESH_TOKEN_ADMIN__',
    userId: 2,
    userName: 'Admin',
    roleId: 'admin',
    password: 'admin123',
		headImageUrl: null, // 头像url
		balance: 0 // 用户余额
  },
  {
    token: '__TOKEN_USER01__',
    refreshToken: '__REFRESH_TOKEN_USER01__',
    userId: 3,
    userName: 'User01',
    roleId: 'user',
    password: 'user01123',
		headImageUrl: null, // 头像url
		balance: 0 // 用户余额
  }
];
