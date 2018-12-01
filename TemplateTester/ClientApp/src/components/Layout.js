import * as React from 'react'
import NavMenu from './NavMenu/NavMenu'

export default props => (
	<React.Fragment>
		<NavMenu />
		<div>
			{props.children}
		</div>
	</React.Fragment>
)
