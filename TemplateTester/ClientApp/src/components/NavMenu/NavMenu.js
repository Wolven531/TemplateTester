import * as React from'react'
import { Link } from 'react-router-dom'

import './NavMenu.css'

export default props => (
	<nav className="nav-menu">
		<ul>
			<li><Link to={'/'}>TemplateTester</Link></li>
			<li><Link to={'/counter'}>Counter</Link></li>
			<li><Link to={'/entity-manager'}>Entity Manager</Link></li>
			<li><Link to={'/idler'}>Idler</Link></li>
			<li><Link to={'/fetchdata'}>Fetch Data</Link></li>
		</ul>
	</nav>
)
